using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class MultiMarkerTracker : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject cubePrefab;

    private Dictionary<string, ARTrackedImage> trackedImages = new Dictionary<string, ARTrackedImage>();
    private GameObject cube;

    void OnEnable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
    }

    void OnDisable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }

    void Start()
    {
        Debug.Log("MultiMarkerTracker started");
        cube = Instantiate(cubePrefab);
        cube.SetActive(false);
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log("Tracked image added: " + trackedImage.referenceImage.name);
            trackedImages[trackedImage.referenceImage.name] = trackedImage;
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            Debug.Log("Tracked image updated: " + trackedImage.referenceImage.name);
            trackedImages[trackedImage.referenceImage.name] = trackedImage;
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            Debug.Log("Tracked image removed: " + trackedImage.referenceImage.name);
            trackedImages.Remove(trackedImage.referenceImage.name);
        }

        UpdateCubeTransform();
    }

    void UpdateCubeTransform()
    {
        if (trackedImages.Count == 6)
        {
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;

            foreach (var trackedImage in trackedImages.Values)
            {
                position += trackedImage.transform.position;
                rotation *= trackedImage.transform.rotation;
            }

            position /= trackedImages.Count;
            cube.transform.position = position;
            cube.transform.rotation = rotation;
            cube.SetActive(true);
        }
        else
        {
            cube.SetActive(false);
        }
    }
}
