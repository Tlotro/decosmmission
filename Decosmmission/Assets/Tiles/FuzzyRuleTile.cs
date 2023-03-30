using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "2D/Tiles/Fuzzy Rule Tile", fileName = "FuzzyRuleTile")]
public class FuzzyRuleTile : RuleTile<FuzzyRuleTile.Neighbor> {

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Null = 3;
        public const int NotNull = 4;
        public const int ThisOrNull = 5;
        public const int Other = 6;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.Null: return tile == null;
            case Neighbor.NotNull: return tile != null;
            case Neighbor.ThisOrNull: return tile == this || tile == null;
            case Neighbor.Other: return tile != this && tile != null;
        }
        return base.RuleMatch(neighbor, tile);
    }
}