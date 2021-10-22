namespace _3K1SPZ_CP.DAL;

public interface ICommentRepository
{
    public void AddComment(int orderId, string commentText);
    public List<CommentDTO> GetCommentsOfUser(int id);
}