using UnityEngine;

public enum TileType
    {
        // None means no type has been allocated to that space yet
        // Rocks are terrain where players cannot build
        // Liquid is for ports / river crossings
        // Transport is for roads / rail. Areas where buildings cannot go
        // Colors represent differents sins/virtues
        None,
        Rock,
        Liquid,
        Road,
        Blue,
        Red,
        Green,
        Yellow,
        Gate
    };
