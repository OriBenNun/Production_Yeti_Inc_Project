using Pool_System;
using UnityEngine;

namespace Game.Ice_Picking
{
    public class IceBlock : PoolGameObject
    {
        public enum IceBlockVariation
        {
            NoSpike,
            LeftSpike,
            RightSpike,
        }

        [SerializeField] private GameObject _leftSpike;
        [SerializeField] private GameObject _rightSpike;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [field: SerializeField] public IceBlockVariation Variation { get; private set; }

        public void Init(Vector3 position, IceBlockVariation variation, Sprite iceBlockSprite)
        {
            transform.position = position;
            Variation = variation;
            _spriteRenderer.sprite = iceBlockSprite;
            SetSpikes();

            gameObject.SetActive(true);
        }

        public override void ResetSelf()
        {
            base.ResetSelf();

            Variation = IceBlockVariation.NoSpike;
        }

        private void SetSpikes()
        {
            switch (Variation)
            {
                case IceBlockVariation.NoSpike:
                    _leftSpike.SetActive(false);
                    _rightSpike.SetActive(false);
                    break;

                case IceBlockVariation.LeftSpike:
                    _leftSpike.SetActive(true);
                    _rightSpike.SetActive(false);
                    break;

                case IceBlockVariation.RightSpike:
                    _leftSpike.SetActive(false);
                    _rightSpike.SetActive(true);
                    break;

                default:
                    _leftSpike.SetActive(false);
                    _rightSpike.SetActive(false);
                    break;
            }
        }

    }
}
