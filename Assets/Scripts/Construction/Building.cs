using Data.Configs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Construction
{
    public class Building : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject builtView;
        [SerializeField] private BuildingConfig config;
        [SerializeField] private Collider colliderComponent;

        private MeshRenderer _renderer;
        private Material _baseMaterial;
        
        public BuildingConfig Config => config;
        public bool IsBuilt;

        private void Awake()
        {
            _renderer = builtView.GetComponent<MeshRenderer>();
            _baseMaterial = _renderer.material;
            builtView.SetActive(false);
            colliderComponent.enabled = false;
        }

        public void Build()
        {
            IsBuilt = true;
            colliderComponent.enabled = false;
            builtView.SetActive(true);
            _renderer.material = _baseMaterial;
        }

        public void SetAsCurrentBuilding()
        {
            builtView.SetActive(true);
            colliderComponent.enabled = true;
            _renderer.material = GenerateTransparentMaterial(_baseMaterial);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"Click on {name}");
            BuildingsManager.Instance.TryBuildBuilding(this);
        }
        
        Material GenerateTransparentMaterial(Material baseMaterial)
        {
            Material transparentMaterial = new Material(baseMaterial);

            transparentMaterial.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            Color color = transparentMaterial.color;
            color.a = 0.25f;
            transparentMaterial.color = color;
        
            return transparentMaterial;
        }
    }
}