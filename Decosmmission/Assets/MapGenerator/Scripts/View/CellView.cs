using UnityEngine;

namespace MapGenerator.Scripts.View
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetColor(Color color)
        {
            _renderer.color = color;
        }
    }
}