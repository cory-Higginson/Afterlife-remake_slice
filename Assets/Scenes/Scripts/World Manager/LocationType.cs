using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        Transport,
        Blue,
        Red,
        Green,
        Yellow
    };
