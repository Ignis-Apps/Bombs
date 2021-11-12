using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.U2D.Animation;

public class PlayerSkinChanger : MonoBehaviour
{
    // Contains the labels of the sprite library asset (PlayerParts)
    [SerializeField] string[] assetLabels;

    [SerializeField] SpriteResolver headResolver;
    
    [SerializeField] SpriteResolver bodyResolver;
    
    [SerializeField] SpriteResolver lArmResolver;
    [SerializeField] SpriteResolver rArmResolver;
    
    [SerializeField] SpriteResolver lLegResolver;
    [SerializeField] SpriteResolver rLegResolver;
    
    [SerializeField] SpriteResolver lShoeResolver;
    [SerializeField] SpriteResolver rShoeResolver;

    [SerializeField] PlayerSkinConfiguration currentConfiguration;


    public void Awake()
    {
        // Apply default configuration
        ApplySkinConfiguration(new PlayerSkinConfiguration());
    }

    public void ApplySkinConfiguration(PlayerSkinConfiguration configuration)
    {
        headResolver.SetCategoryAndLabel(headResolver.GetCategory(), assetLabels[configuration.headID]);

        bodyResolver.SetCategoryAndLabel(bodyResolver.GetCategory(), assetLabels[configuration.bodyID]);

        lArmResolver.SetCategoryAndLabel(lArmResolver.GetCategory(), assetLabels[configuration.leftArmID]);
        rArmResolver.SetCategoryAndLabel(rArmResolver.GetCategory(), assetLabels[configuration.rightArmID]);

        lLegResolver.SetCategoryAndLabel(lLegResolver.GetCategory(), assetLabels[configuration.leftLegID]);
        rLegResolver.SetCategoryAndLabel(rLegResolver.GetCategory(), assetLabels[configuration.rightLegID]);

        lShoeResolver.SetCategoryAndLabel(lShoeResolver.GetCategory(), assetLabels[configuration.leftShoeID]);
        rShoeResolver.SetCategoryAndLabel(rShoeResolver.GetCategory(), assetLabels[configuration.rightShoeID]);

        currentConfiguration = configuration;
    }

    public PlayerSkinConfiguration GetPlayerSkinConfiguration()
    {
        return currentConfiguration;
    }

    public void nextHead() {
        currentConfiguration.headID = (currentConfiguration.headID + 1) % assetLabels.Length;
        ApplySkinConfiguration(currentConfiguration);
    }
    public void previousHead() {
        currentConfiguration.headID = (currentConfiguration.headID - 1 + assetLabels.Length) % assetLabels.Length;
        ApplySkinConfiguration(currentConfiguration);
    }
    public void nextUpperBody() {
        currentConfiguration.bodyID = (currentConfiguration.bodyID + 1) % assetLabels.Length;
        currentConfiguration.SetArms((currentConfiguration.rightArmID + 1) % assetLabels.Length);
        ApplySkinConfiguration(currentConfiguration);
    }
    public void previousUpperBody() {
        currentConfiguration.bodyID = (currentConfiguration.bodyID - 1 + assetLabels.Length) % assetLabels.Length;
        currentConfiguration.SetArms((currentConfiguration.rightArmID - 1 + assetLabels.Length) % assetLabels.Length);
        ApplySkinConfiguration(currentConfiguration);
    }
    public void nextLegs() {
        currentConfiguration.SetLegs((currentConfiguration.rightLegID + 1) % assetLabels.Length);
        ApplySkinConfiguration(currentConfiguration);
    }
    public void previousLegs() {
        currentConfiguration.SetLegs((currentConfiguration.rightLegID - 1 + assetLabels.Length) % assetLabels.Length);
        ApplySkinConfiguration(currentConfiguration);
    }
    public void nextShoes()
    {
        currentConfiguration.SetShoes((currentConfiguration.rightShoeID + 1) % assetLabels.Length);
        ApplySkinConfiguration(currentConfiguration);
    }
    public void previousShoes()
    {
        currentConfiguration.SetShoes((currentConfiguration.rightShoeID - 1 + assetLabels.Length) % assetLabels.Length);
        ApplySkinConfiguration(currentConfiguration);
    }

    public void nextFullSkin() {
        currentConfiguration.SetAll((currentConfiguration.headID + 1) % assetLabels.Length);
    }

    public void previousFullSkin() {
        currentConfiguration.SetAll((currentConfiguration.headID - 1 + assetLabels.Length) % assetLabels.Length);
    }

    public class PlayerSkinConfiguration
    {
        public int headID { get; set; }

        public int bodyID { get; set; }

        public int leftArmID { get; set; }
        public int rightArmID { get; set; }

        public int leftLegID { get; set; }
        public int rightLegID { get; set; }

        public int leftShoeID { get; set; }
        public int rightShoeID { get; set; }

        public void SetArms(int id)
        {
            leftArmID = id;
            rightArmID = id;
        }

        public void SetLegs(int id)
        {
            leftLegID = id;
            rightLegID = id;
        }

        public void SetShoes(int id)
        {
            leftShoeID = id;
            rightShoeID = id;
        }
        public void SetAll(int id)
        {
            headID = id;
            bodyID = id;
            SetArms(id);
            SetLegs(id);
            SetShoes(id);
        }

        public void Load(string data)
        {
            string[] d = data.Split(',');
            
            Assert.IsTrue(d.Length == 8);

            headID = int.Parse(d[0]);          
            bodyID = int.Parse(d[1]);
            leftArmID = int.Parse(d[2]);
            rightArmID = int.Parse(d[3]);
            leftLegID = int.Parse(d[4]);
            rightLegID = int.Parse(d[5]);
            leftShoeID = int.Parse(d[6]);
            rightShoeID = int.Parse(d[7]);
        }

        public string Save()
        {
            return headID + ","
                + bodyID + "," 
                + leftArmID+ "," 
                + rightArmID+ "," 
                + leftLegID+ "," 
                + rightLegID+ "," 
                + leftShoeID+ ","
                + rightShoeID;
        }

    }
}
