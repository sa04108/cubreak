#include <fstream>
#include <iostream>
#include <nlohmann/json.hpp>
#include <random>
#include <sstream>
#include <stack>

using json = nlohmann::json;
using Coord = std::tuple<int, int, int>;

// ENUM_COLOR equivalent: 0..7
enum ENUM_COLOR { RED = 0, GREEN, BLUE, YELLOW, MAGENTA, CYAN, BLACK, WHITE };

// Helper: convert ENUM_COLOR to string
std::string ColorToString(int colorIdx) {
    switch (colorIdx) {
    case RED: return "RED";
    case GREEN: return "GREEN";
    case BLUE: return "BLUE";
    case YELLOW: return "YELLOW";
    case MAGENTA: return "MAGENTA";
    case CYAN: return "CYAN";
    case BLACK: return "BLACK";
    case WHITE: return "WHITE";
    default: return "UNKNOWN";
    }
}

// Generate random colors indices 0..7, choose n distinct if needed
std::vector<int> GetRandomColorIndices(int n) {
    int m = 8;
    if (n < 0 || n > m) throw std::out_of_range("n must be between 0 and 8");
    std::vector<int> indices(m);
    for (int i = 0; i < m; i++) indices[i] = i;
    std::random_device rd;
    std::mt19937 gen(rd());
    for (int i = 0; i < n; i++) {
        std::uniform_int_distribution<> dis(i, m - 1);
        int swapIdx = dis(gen);
        std::swap(indices[i], indices[swapIdx]);
    }
    indices.resize(n);
    return indices;
}

// CubeStage structure
struct CubeStage {
    int id;
    int dimension;
    std::vector<std::vector<std::vector<int>>> grid; // [x][y][z], 0 means empty, >0 means color+1
};

// Check if grid is empty
bool IsEmpty(const std::vector<std::vector<std::vector<int>>>& grid) {
    int N = grid.size();
    for (int x = 0; x < N; x++)
        for (int y = 0; y < N; y++)
            for (int z = 0; z < N; z++)
                if (grid[x][y][z] != 0) return false;
    return true;
}

// Clone grid
auto CloneGrid(const std::vector<std::vector<std::vector<int>>>& original) {
    return original;
}

// Apply gravity: for each (x,y), drop non-zero to bottom (z=0)
void ApplyGravity(std::vector<std::vector<std::vector<int>>>& grid) {
    int N = grid.size();
    for (int x = 0; x < N; x++) {
        for (int y = 0; y < N; y++) {
            std::vector<int> stack;
            for (int z = 0; z < N; z++) {
                if (grid[x][y][z] != 0) {
                    stack.push_back(grid[x][y][z]);
                    grid[x][y][z] = 0;
                }
            }
            for (int i = 0; i < stack.size(); i++) {
                grid[x][y][i] = stack[i];
            }
        }
    }
}

// Find all connected groups of same color
std::vector<std::vector<Coord>> FindAllGroups(const std::vector<std::vector<std::vector<int>>>& grid) {
    int N = grid.size();
    std::vector<std::vector<std::vector<bool>>> visited(N, std::vector<std::vector<bool>>(N, std::vector<bool>(N, false)));
    std::vector<std::vector<Coord>> groups;
    int dx[6] = { 1, -1, 0, 0, 0, 0 };
    int dy[6] = { 0, 0, 1, -1, 0, 0 };
    int dz[6] = { 0, 0, 0, 0, 1, -1 };

    for (int x = 0; x < N; x++) {
        for (int y = 0; y < N; y++) {
            for (int z = 0; z < N; z++) {
                if (grid[x][y][z] == 0 || visited[x][y][z]) continue;
                int color = grid[x][y][z];
                std::stack<Coord> stk;
                stk.push({ x, y, z });
                visited[x][y][z] = true;
                std::vector<Coord> component;
                while (!stk.empty()) {
                    auto [cx, cy, cz] = stk.top(); stk.pop();
                    component.push_back({ cx, cy, cz });
                    for (int dir = 0; dir < 6; dir++) {
                        int nx = cx + dx[dir];
                        int ny = cy + dy[dir];
                        int nz = cz + dz[dir];
                        if (nx < 0 || nx >= N || ny < 0 || ny >= N || nz < 0 || nz >= N) continue;
                        if (visited[nx][ny][nz]) continue;
                        if (grid[nx][ny][nz] == color) {
                            visited[nx][ny][nz] = true;
                            stk.push({ nx, ny, nz });
                        }
                    }
                }
                groups.push_back(component);
            }
        }
    }
    return groups;
}

// Remove group from grid (set to 0)
void RemoveGroup(std::vector<std::vector<std::vector<int>>>& grid, const std::vector<Coord>& group) {
    for (auto& coord : group) {
        auto [x, y, z] = coord;
        grid[x][y][z] = 0;
    }
}

// Solve by brute force: return true if clearable
bool SolveBruteForce(std::vector<std::vector<std::vector<int>>>& grid) {
    if (IsEmpty(grid)) return true;
    auto allGroups = FindAllGroups(grid);
    // Filter out single-block groups
    std::vector<std::vector<Coord>> largeGroups;
    for (auto& g : allGroups) if (g.size() > 1) largeGroups.push_back(g);
    if (largeGroups.empty()) return false;
    // Sort groups: no heuristic here, just try each
    for (auto& group : largeGroups) {
        auto next = CloneGrid(grid);
        RemoveGroup(next, group);
        ApplyGravity(next);
        if (SolveBruteForce(next)) return true;
    }
    return false;
}

// Check if clearable
bool IsClearable(const std::vector<std::vector<std::vector<int>>>& grid) {
    auto copy = CloneGrid(grid);
    return SolveBruteForce(copy);
}

// Attempt to fix by swapping two blocks up to maxAttempt
bool Fix(std::vector<std::vector<std::vector<int>>>& grid, int maxAttempt = 20) {
    int N = grid.size();
    std::vector<Coord> filledCoords;
    for (int x = 0; x < N; x++)
        for (int y = 0; y < N; y++)
            for (int z = 0; z < N; z++)
                if (grid[x][y][z] != 0)
                    filledCoords.push_back({ x, y, z });
    if (filledCoords.empty()) return false;
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<> dis(0, filledCoords.size() - 1);
    int attempt = 0;
    while (attempt < maxAttempt) {
        attempt++;
        int idxA = dis(gen);
        int idxB = dis(gen);
        if (idxA == idxB) continue;
        auto [ax, ay, az] = filledCoords[idxA];
        auto [bx, by, bz] = filledCoords[idxB];
        if (grid[ax][ay][az] == grid[bx][by][bz]) continue;
        std::swap(grid[ax][ay][az], grid[bx][by][bz]);
        if (IsClearable(grid)) {
            std::cout << "Fixed after " << attempt << " attempts\n";
            return true;
        }
        std::swap(grid[ax][ay][az], grid[bx][by][bz]);
    }
    return false;
}

// Generate random cube stage
std::vector<std::vector<std::vector<int>>> GenerateRandomGrid(int N, int colorCount) {
    std::random_device rd;
    std::mt19937 gen(rd());
    // 1) 먼저 사용할 색상 인덱스 리스트를 얻는다
    auto colorIndices = GetRandomColorIndices(colorCount);
    std::uniform_int_distribution<> dis(0, colorCount - 1); // 인덱스로 사용할 distribution

    std::vector<std::vector<std::vector<int>>> grid(N, std::vector<std::vector<int>>(N, std::vector<int>(N)));
    for (int x = 0; x < N; x++) {
        for (int y = 0; y < N; y++) {
            for (int z = 0; z < N; z++) {
                int pickIdx = dis(gen);
                int colorIdx = colorIndices[pickIdx];
                grid[x][y][z] = colorIdx + 1; // 저장할 때는 +1
            }
        }
    }
    return grid;
}

// Read configuration from 'stage-maker.config'
void ReadConfig(int& N, int& colorCount, int& maxStages, int& maxRepeats) {
    std::ifstream configFile("stage-maker.config");
    if (!configFile.is_open()) {
        std::cerr << "Could not open stage-maker.config; using defaults.\n";
        return;
    }
    std::string line;
    while (std::getline(configFile, line)) {
        if (line.empty() || line[0] == '#') continue;
        std::istringstream iss(line);
        std::string key;
        if (std::getline(iss, key, '=')) {
            std::string value;
            if (std::getline(iss, value)) {
                if (key == "N") {
                    N = std::stoi(value);
                }
                else if (key == "colorCount") {
                    colorCount = std::stoi(value);
                }
                else if (key == "maxStages") {
                    maxStages = std::stoi(value);
                }
                else if (key == "maxRepeats") {
                    maxRepeats = std::stoi(value);
                }
            }
        }
    }
    configFile.close();
}

// Save multiple CubeStages to JSON file
void SaveStagesToJson(const std::vector<CubeStage>& stages, const std::string& filename) {
    json jArray = json::array();
    for (const auto& stage : stages) {
        json jStage;
        jStage["id"] = stage.id;
        jStage["dimension"] = stage.dimension;
        // layers: convert grid to layer-wise arrangements
        std::vector<json> layersJson;
        for (int z = 0; z < stage.dimension; z++) {
            json layerJson;
            layerJson["index"] = z;
            std::map<int, std::vector<int>> dict; // color -> positions
            for (int y = 0; y < stage.dimension; y++) {
                for (int x = 0; x < stage.dimension; x++) {
                    int raw = stage.grid[x][y][z];
                    int colorIdx = raw - 1;
                    int pos = y * stage.dimension + x + 1;
                    dict[colorIdx].push_back(pos);
                }
            }
            std::vector<json> arrangements;
            for (auto& kv : dict) {
                json arr;
                arr["color"] = ColorToString(kv.first);
                arr["positions"] = kv.second;
                arrangements.push_back(arr);
            }
            layerJson["arrangements"] = arrangements;
            layersJson.push_back(layerJson);
        }
        jStage["layers"] = layersJson;
        jArray.push_back(jStage);
    }
    std::ofstream o(filename);
    o << std::setw(4) << jArray << std::endl;
    o.close();
}

int main() {
    int N = 3;           // 기본값: 큐브 한 변 길이
    int colorCount = 4;  // 기본값: 큐브에 사용할 색상 개수 (0~8 중 랜덤 선택)
    int maxStages = 1;   // 기본값: 가져오고자 하는 스테이지(큐브) 최대 개수
    int maxRepeats = 10; // 기본값: 반복 횟수

    if (colorCount > 8)
        colorCount = 8;

    ReadConfig(N, colorCount, maxStages, maxRepeats);

    std::vector<CubeStage> foundStages;
    int stageId = 1;

    for (int repeat = 0; repeat < maxRepeats; repeat++) {
        // 찾은 스테이지 수가 목표 스테이지 수를 초과하면 반복 종료
        if (maxStages <= foundStages.size())
            break;

        auto grid = GenerateRandomGrid(N, colorCount);
        if (IsClearable(grid) || Fix(grid)) {
            CubeStage stage;
            stage.id = stageId++;
            stage.dimension = N;
            stage.grid = grid;
            foundStages.push_back(stage);

            std::cout << "+ Repeat " << repeat + 1 << ": Found.\n";
        }
        else
        {
            std::cout << "- Repeat " << repeat + 1 << ": Not found.\n";
        }
    }

    if (foundStages.empty()) {
        std::cout << "No clearable stages found within " << maxRepeats << " repeats.\n";
        return 1;
    }

    // Save all found stages to JSON
    SaveStagesToJson(foundStages, "stage_data_ex.json");
    std::cout << "Saved " << foundStages.size() << " stages to stage_data_ex.json" << std::endl;
    return 0;
}
