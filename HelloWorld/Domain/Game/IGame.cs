using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld;

public interface IGame
{
    Task Lancer(CancellationToken ct = default);
}