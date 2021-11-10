using System.Drawing;
using TowerDefence_ClientSide;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Soldiers;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ServerSide.Facade
{
    public class PatternFacade
    {
        private static PatternFacade instance;

        private static MapFactory mapFactory;
        private static GameElementFactory towerFactory;

        private static Barrack barrack;
        private static SoldierBuilder builder;

        private static Command cursorCommand;

        public static PatternFacade GetInstance()
        {
            return instance;
        }

        public static void CreateInstance()
        {
            if (instance != null) return;
            mapFactory = new MapFactory();
            towerFactory = new TowerFactory();
            barrack = new Barrack();

            instance = new PatternFacade();
        }

        public void InitCursorCommand(GameCursor gameCursor)
        {
            cursorCommand = new CursorCommand(gameCursor);
        }

        public Map CreateMap(string mapType)
        {
            return mapFactory.CreateMap(mapType);
        }

        public Tower CreateTower(PlayerType playerType, TowerType towerType, Point point)
        {
            return towerFactory.CreateTower(playerType, towerType, point);
        }

        public Soldier TrainSoldier(PlayerType playerType, SoldierType soldierType)
        {
            switch (soldierType)
            {
                case SoldierType.HitpointsSoldier:
                    builder = new HitpointsSoldierBuilder(playerType, soldierType, 1);
                    barrack.Train(builder, playerType);
                    return builder.Soldier;
                case SoldierType.SpeedSoldier:
                    builder = new SpeedSoldierBuilder(playerType, soldierType, 1);
                    barrack.Train(builder, playerType);
                    return builder.Soldier;
                default:
                    return null;
            }
        }  
        
        public void DoCommand(TowerType towerType)
        {
            cursorCommand.Do(towerType);
        }

        public void UndoCommand()
        {
            cursorCommand.Undo();
        }
    }
}
