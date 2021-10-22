namespace _3K1SPZ_CP.DAL;

public class CommentDTO
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Text { get; set; }
    public DateTime InsertTime { get; set; }
    public DateTime UpdateTime { get; set; }
}