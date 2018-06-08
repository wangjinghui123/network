using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankItem : MonoBehaviour
{
    [SerializeField] private Text _rankText;
    [SerializeField] private Image _headImage;
    [SerializeField] private Text _nikeNameText;
    [SerializeField] private Text _scoreText;
    public Text rankText
    {
        get
        {
            return _rankText;
        }
        set
        {
            _rankText = value;
        }

    }
    public Image headImage
    {
        get
        {
            return _headImage;
        }
        set
        {
            _headImage = value;
        }
    }
    public Text nikeNameText
    {
        get
        {
            return _nikeNameText;
        }
        set
        {
            _nikeNameText = value;
        }
    }
    public Text scoreText
    {
        get
        {
            return _scoreText;
        }
        set
        {
            _scoreText = value;
        }
    }




}
