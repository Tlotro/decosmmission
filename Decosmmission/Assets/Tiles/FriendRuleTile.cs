using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "2D/Tiles/Friend Rule Tile", fileName = "FriendRuleTile")]
public class FriendRuleTile : RuleTile<FriendRuleTile.Neighbor> {
    public List<TileBase> siblings = new List<TileBase>();
    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Sibling = 3;
        public const int NotSibling = 4;
        public const int SiblingOrThis = 5;
        public const int Other = 6;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.Sibling: return siblings.Contains(tile);
            case Neighbor.NotSibling: return !siblings.Contains(tile);
            case Neighbor.SiblingOrThis: return tile == this || siblings.Contains(tile);
            case Neighbor.Other: return tile != this && !siblings.Contains(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }
}