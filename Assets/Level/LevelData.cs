//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace Level
//{
//    [CreateAssetMenu(fileName = "Level Data", menuName = "Level/Level Data")]
//    public class LevelData : ScriptableObject
//    {
//        [SerializeField] private int width;
//        public int Width { get { return width; } }

//        [SerializeField] private int height;
//        public int Height { get { return height; } }


//        [SerializeField] private GameAssetData[] gameAssetDatas;
//        public GameAssetData[] GameAssetDatas { get { return gameAssetDatas; } }
//    }

//    [System.Serializable]
//    public struct GameAssetData
//    {
//        [SerializeField] private int x;
//        public int X { get { return x; } }

//        [SerializeField] private int y;
//        public int Y { get { return y; } }

//        [SerializeReference]
//        [SerializeField] private IGameAsset gameAsset;
//        public IGameAsset GameAsset { get { return gameAsset; } }

//        [SerializeReference]
//        [SerializeField] private GameAssetProperty[] properties;
//        public GameAssetProperty[] Properties { get { return properties; } }

//        public void Load()
//        {

//        }
//    }

//    [System.Serializable]
//    public class GameAssetLoader
//    {

//    }

//    [System.Serializable]
//    public abstract class GameAssetProperty
//    {
//        [SerializeField] private GameAssetPropertyType type;
//        public GameAssetPropertyType Type { get { return type; } }
//    }

//    public class GameAssetProperty_Bool : GameAssetProperty
//    {

//    }

//    public struct GameAssetPropertyCollection
//    {
//        public GameAssetProperty[] Properties { get; private set; }

//        public GameAssetPropertyCollection(params GameAssetProperty[] properties)
//        {
//            this.Properties = properties;
//        }

//    }

//    public enum GameAssetPropertyType
//    {
//        Bool,
//        Int,
//        String
//    }

//    public interface IGameAsset
//    {
//        void Load(GameAssetLoader gameAssetLoader);
//    }
//}
