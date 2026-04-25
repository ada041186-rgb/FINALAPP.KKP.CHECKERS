namespace CHECKERS.Services
{
    public interface ITurnSwitcher
    {
        void Switch(IStateContext ctx);
    }
}