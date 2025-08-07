using TastyCore.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace TastyCore.Utils
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(GridLayoutGroup))]
    public class AdjustGridLayoutCellSize : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private Axis _expand;
        [SerializeField] private RatioMode _ratioMode;
        [SerializeField] private float _cellRatio = 1;
 
        [Header("Grid Layout")]
    
        [SerializeField] private RectTransform _rectTransform;
    
        [SerializeField]GridLayoutGroup _gridLayout;
 
    
        void Start()
        {
            UpdateCellSize();
        }
 
        void OnRectTransformDimensionsChange()
        {
            UpdateCellSize();
        }
 
#if UNITY_EDITOR
        [ExecuteAlways]
        void Update()
        {
            UpdateCellSize();
        }
#endif
 
        void OnValidate()
        {
            Debug.Log("VALIDATE: " + _rectTransform.rect.width);
            _gridLayout = GetComponent<GridLayoutGroup>();
            UpdateCellSize();
        }
 
        void UpdateCellSize()
        {
            var count = _gridLayout.constraintCount;
            float spacing, contentSize, sizePerCell;
            
            switch (_expand)
            {
                case Axis.X:
                    spacing = (count - 1) * _gridLayout.spacing.x;
                    contentSize = _rectTransform.rect.width - _gridLayout.padding.left - _gridLayout.padding.right - spacing;
                    sizePerCell = contentSize / count;
                    _gridLayout.cellSize = new Vector2(sizePerCell, _ratioMode == RatioMode.Free ? _gridLayout.cellSize.y : sizePerCell * _cellRatio);
                    break;
                
                case Axis.Y:
                    spacing = (count - 1) * _gridLayout.spacing.y;
                    contentSize = _rectTransform.rect.height - _gridLayout.padding.top - _gridLayout.padding.bottom -spacing;
                    sizePerCell = contentSize / count;
                    _gridLayout.cellSize = new Vector2(_ratioMode == RatioMode.Free ? _gridLayout.cellSize.x : sizePerCell * _cellRatio, sizePerCell);
                    break;
                
                case Axis.Z:
                default:
                    break;
            }
        }
    }
}
