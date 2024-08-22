using UnityEngine;

namespace Controllers
{
    public interface ISaveLoad
    {
        public string BoardDataKey { get;}
        void Save(BoardData boardData);
        BoardData Load();
    }

    public class SaveLoadSystem : ISaveLoad
    {
        public string BoardDataKey => "Board Data Key";

        public BoardData BoardData;

        public void Save(BoardData boardData)
        {
            BoardData = boardData;
        }

        public BoardData Load()
        {
            return BoardData;
        }
    }
}