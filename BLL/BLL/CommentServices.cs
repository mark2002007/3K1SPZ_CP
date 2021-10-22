

using _3K1SPZ_CP;
using _3K1SPZ_CP.DAL;

public class CommentServices
{
    private CommentRepository commentRepository { get; set; }
    public CommentServices()
    {
        commentRepository = new CommentRepository(Helper.CnnVal());
    }
    public void AddComment(int orderId, string commentText) => commentRepository.AddComment(orderId, commentText);
    public List<CommentDTO> GetCommentsOfUser(int id) => commentRepository.GetCommentsOfUser(id);
}