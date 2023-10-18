using TMPro;
using UnityEngine;

namespace GameView.View
{
    public class BaseUIItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textCount;

        public void SetCount(int count, int maxCount)
        {
            _textCount.text = $"{count.ToString()} / {maxCount.ToString()}";
        }
    }
}