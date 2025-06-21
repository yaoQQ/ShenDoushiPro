using UnityEngine;
namespace StarterAssets
{
    public class OutSpaceUICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public OutSpaceStarterAssetsInputs starterAssetsInputs;
        private void Awake()
        {
            Debug.Log("==============OutSpaceUICanvasControllerInput Awake");
        }
        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            Debug.Log("VirtualMoveInput virtualMoveDirection=" + virtualMoveDirection);
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualFastGunInput(bool isFastGun)
        {
            Debug.Log("@@@@VirtualMoveInput VirtualFastGunInput=" + isFastGun);
            starterAssetsInputs.FastGunInput(isFastGun);
        }

        public void VirtualDefendGunInput(bool isDefendGun)
        {
            Debug.Log("@@@@VirtualDefendGunInput isDefendGun=" + isDefendGun);
            starterAssetsInputs.DefendGunInput(isDefendGun);
        }

        public void VirtualAOEGunInput(bool isAOEGun)
        {
            starterAssetsInputs.AOEGunInput(isAOEGun);
        }
        public void VirtualSkillDisturbGunInput(bool isSkill)
        {
            starterAssetsInputs.SkillDisturbGunInput(isSkill);
        }
    }
}