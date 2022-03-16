using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourcesLoader : MonoBehaviour
{
    [SerializeField] private Transform _prefabRoot;

    private GameObject _player;
    private Renderer[] _infoPlayer;
    private string[] _hareNames;
    private string _prefabName;

    private void Start()
    {
        _prefabName = "Male A";
        LoadPrefab();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _prefabName = "Female A";
            LoadPrefab();
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            _prefabName = "Male A";
            LoadPrefab();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(_prefabName == "Male A")
                ChooseColorHair("Hair_2");
            if(_prefabName == "Female A")
                ChooseColorHair("Female_Hair_4");
        }
    }

    private void LoadPrefab()
    {
        if (_player != null)
        {
            Destroy(_player);
        }
            
        var prefab = Resources.Load<GameObject>($"Prefabs/{_prefabName}");
        _player = Instantiate(prefab, _prefabRoot);
        _infoPlayer = _player.GetComponentsInChildren<Renderer>();
    }

    private void ChooseColorHair(string typeHair)
    {
        var directoryMaterials = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath,"Materials","Hairs",typeHair));
        var allFiles = directoryMaterials.GetFiles("*.*");
        
        var bytes = File.ReadAllBytes(allFiles[Random.Range(0,allFiles.Length)].FullName);
        var texture2D = new Texture2D(1, 1);

        texture2D.LoadImage(bytes);

        _infoPlayer.First(s => s.name.Contains("Hair")).gameObject.GetComponent<SkinnedMeshRenderer>()
            .sharedMaterial.mainTexture = texture2D;
    }
}
