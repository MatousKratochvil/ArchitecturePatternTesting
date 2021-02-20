namespace Eshop.Base.Abstraction.Domain
{
	/// <summary>
	/// Domain aggregate root.
	/// </summary>
	/// <typeparam name="TId">Identification type</typeparam>
	public interface IAggregateRoot<TId>
		where TId : struct
	{
		TId Id { get; set; }
	}
}