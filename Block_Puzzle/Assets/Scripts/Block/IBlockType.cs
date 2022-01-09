public interface IBlockType
{
    public enum BLOCK_TYPE {
        PATTERNED,
        RANDOMIZED,
        UNDEFINED
    }

    void SelectBlockType();
    void ResetBlockColor();
}
