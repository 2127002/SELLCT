using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCollectionView : MonoBehaviour
{
    [SerializeField] List<Image> _collectionImages = default!;

    private void Start()
    {
        for (int i = 0; i < _collectionImages.Count; i++)
        {
            _collectionImages[i].enabled = DataManager.saveData.hasCollectedEndings[i];
        }
    }
}
