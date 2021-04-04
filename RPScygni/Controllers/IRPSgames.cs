using RPScygni.Models;

namespace RPScygni.Controllers
{
    public interface IRPSgames
    {
        Response CreateGame(Request request);
        Response CheckGameStatus(Request request);
        Response PlayGame(Request request);
        Response JoinGame(Request request);
    }
}
