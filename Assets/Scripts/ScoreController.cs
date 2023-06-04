using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private PlayerController _playerController;
    private TextMesh _textMesh;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        _textMesh.text = _playerController.GetScore().ToString();
    }
}
