namespace Neomaster.PixelEarth.Domain;

public interface IIdGenerator<TId>
{
  TId Next();
}
