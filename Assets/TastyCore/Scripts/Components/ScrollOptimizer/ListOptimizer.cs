using System;
using System.Collections.Generic;
using TastyCore.Enums;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace TastyCore.Components.ScrollOptimizer
{
    public abstract class ListOptimizer<TItem, TContext> : MonoBehaviour where TItem : ListItem<TContext>
    {
        [Header("Scroll view components")] 
        [SerializeField] private ScrollRect _scrollView;
        [SerializeField] private Mask _mask;

        [Header("Items config")]
        [SerializeField] private RectTransform _prefab;
        [SerializeField] private float _spacing;

        [Header("Scroll config")]
        [SerializeField] private Orientation _direction = Orientation.Horizontal;

        private RectTransform _container;
        private RectTransform _maskRT;
        
        private int _numVisible;
        private int _numBuffer = 2;
        private float _containerHalfSize;
        private float _prefabSize;

        private Dictionary<int, int[]> _itemDict = new Dictionary<int, int[]>();

        private List<RectTransform> _listItemRect = new List<RectTransform>();
        private List<TItem> _listItems = new List<TItem>();

        private int _num;
        private int _numItems = 0;
        private Vector3 _startPos;
        private Vector3 _offsetVec;
        private bool _isCreated = false;
        
        protected void Initialize(int size)
        {
            switch(_direction)
            {
                case Orientation.Horizontal:
                    _scrollView.vertical = false;
                    _scrollView.horizontal = true;
                    break;
                
                case Orientation.Vertical:
                    _scrollView.horizontal = false;
                    _scrollView.vertical = true;
                    break;
            }

            _container = _scrollView.content;
            
            Clear();
            
            _num = size;
            _container.anchoredPosition3D = new Vector3(0, 0, 0);
            _maskRT = _mask.GetComponent<RectTransform>();

            var prefabScale = _prefab.rect.size;
            _prefabSize = (_direction == Orientation.Horizontal ? prefabScale.x : prefabScale.y) + _spacing;

            _container.sizeDelta = _direction == Orientation.Horizontal
                ? (new Vector2(_prefabSize * _num, prefabScale.y))
                : (new Vector2(prefabScale.x, _prefabSize * _num));
            
            _containerHalfSize = _direction == Orientation.Horizontal
                ? (_container.rect.size.x * 0.5f)
                : (_container.rect.size.y * 0.5f);

            _numVisible =
                Mathf.CeilToInt((_direction == Orientation.Horizontal ? _maskRT.rect.size.x : _maskRT.rect.size.y) /
                                _prefabSize);

            _offsetVec = _direction == Orientation.Horizontal ? Vector3.right : Vector3.down;
            _startPos = _container.anchoredPosition3D - (_offsetVec * _containerHalfSize) + (_offsetVec *
                ((_direction == Orientation.Horizontal ? prefabScale.x : prefabScale.y) * 0.5f));

            _numItems = Mathf.Min(_num, _numVisible + _numBuffer);
            
            for (var i = 0; i < _numItems; i++)
            {
                var obj = Instantiate(_prefab.gameObject, _container.transform);
                var t = obj.GetComponent<RectTransform>();
                t.anchoredPosition3D = _startPos + (_offsetVec * i * _prefabSize);
                _listItemRect.Add(t);
                _itemDict.Add(t.GetInstanceID(), new int[] { i, i });
                obj.SetActive(true);

                var li = obj.GetComponentInChildren<TItem>();
                _listItems.Add(li);
                li.UpdateContent(i, GetContext(i));
            }

            _prefab.gameObject.SetActive(false);
            _container.anchoredPosition3D += _offsetVec * (_containerHalfSize -
                                                           ((_direction == Orientation.Horizontal
                                                               ? _maskRT.rect.size.x
                                                               : _maskRT.rect.size.y) * 0.5f));
            _isCreated = true;
        }

        protected void Clear()
        {
            foreach (var item in _listItems)
            {
                Destroy(item.gameObject);
            }

            _listItems.Clear();
            _listItemRect.Clear();
            _isCreated = false;
        }
        
        public void ReorderItems(float normPos)
        {
            if (!_isCreated) return;

            if (_direction == Orientation.Vertical) normPos = 1f - normPos;
            int numOutOfView =
                Mathf.CeilToInt(normPos * (_num - _numVisible)); //number of elements beyond the left boundary (or top)
            int firstIndex =
                Mathf.Max(0, numOutOfView - _numBuffer); //index of first element beyond the left boundary (or top)
            int originalIndex = firstIndex % _numItems;

            int newIndex = firstIndex;
            for (int i = originalIndex; i < _numItems; i++)
            {
                MoveItemByIndex(_listItemRect[i], newIndex);
                _listItems[i].UpdateContent(newIndex, GetContext(newIndex));
                newIndex++;
            }

            for (int i = 0; i < originalIndex; i++)
            {
                MoveItemByIndex(_listItemRect[i], newIndex);
                _listItems[i].UpdateContent(newIndex, GetContext(newIndex));
                newIndex++;
            }
        }

        private void MoveItemByIndex(RectTransform item, int index)
        {
            var id = item.GetInstanceID();
            _itemDict[id][0] = index;
            item.anchoredPosition3D = _startPos + (_offsetVec * index * _prefabSize);
        }

        protected abstract TContext GetContext(int index);

    }
}