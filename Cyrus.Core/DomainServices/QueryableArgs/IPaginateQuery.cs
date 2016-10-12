namespace Cyrus.Core.DomainServices
{
    public interface IPaginateQuery<T> : IOrderByQuery<T>, ITakeQuery
    {
        int PageIndex { get; }
    }
}
