using System.Drawing;
using TowerDefence_ClientSide;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ServerSide.Facade
{
    public class PatternFacade
    {
        private static PatternFacade _instance;

        private static MapFactory _mapFactory;
        private static GameElementFactory _towerFactory;

        private static Barrack _barrack;
        private static SoldierBuilder _builder;

        private static Command _cursorCommand;

        public static PatternFacade GetInstance()
        {
            return _instance;
        }

        public static void CreateInstance()
        {
            if (_instance != null) return;
            _mapFactory = new MapFactory();
            _towerFactory = new TowerFactory();
            _barrack = new Barrack();

            _instance = new PatternFacade();
        }

        public void InitCursorCommand(GameCursor gameCursor)
        {
            _cursorCommand = new CursorCommand(gameCursor);
        }

        public Map CreateMap(string mapType)
        {
            return _mapFactory.CreateMap(mapType);
        }

        public Tower CreateTower(PlayerType playerType, TowerType towerType, Point point)
        {
            return _towerFactory.CreateTower(playerType, towerType, point);
        }

        public Soldier TrainSoldier(PlayerType playerType, SoldierType soldierType)
        {
            switch (soldierType)
            {
                case SoldierType.HitpointsSoldier:
                    _builder = new HitpointsSoldierBuilder(playerType, soldierType, 0);
                    _barrack.Train(_builder, playerType);
                    return _builder.Soldier;
                case SoldierType.SpeedSoldier:
                    _builder = new SpeedSoldierBuilder(playerType, soldierType, 0);
                    _barrack.Train(_builder, playerType);
                    return _builder.Soldier;
                default:
                    return null;
            }
        }  
        
        public void DoCommand(TowerType towerType)
        {
            _cursorCommand.Do(towerType);
        }

        public void UndoCommand()
        {
            _cursorCommand.Undo();
        }
    }
}
