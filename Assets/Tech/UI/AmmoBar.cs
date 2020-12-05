using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : Singleton<AmmoBar>
{
    public Image ammoImage;

    public List<Image> ammoImages;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveAmmo(int amount)
    {
        Image imageToRemove = ammoImages[ammoImages.Count - 1];
        ammoImages.Remove(imageToRemove);
        Destroy(imageToRemove.gameObject);
    }

    public void FillUpAmmo(int maxAmmo)
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            Image spawnedImage = Instantiate(ammoImage, this.transform);
            ammoImages.Add(spawnedImage);
        }
    }

    public void ClearAmmo()
    {
        foreach (Image i in ammoImages)
        {
            Destroy(i.gameObject);
        }
        ammoImages.Clear();
    }
}
