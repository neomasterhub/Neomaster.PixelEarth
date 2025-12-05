namespace Neomaster.PixelEarth.Infra;

public interface IIdGenerator<TId>
{
  TId Next();
}
