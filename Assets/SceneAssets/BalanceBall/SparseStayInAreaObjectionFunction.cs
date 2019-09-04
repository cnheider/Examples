﻿using System;
using droid.Runtime.Prototyping.Evaluation;
using droid.Runtime.Prototyping.Evaluation.Spatial;

namespace SceneAssets.BalanceBall {
  /// <summary>
  ///
  /// </summary>
  public class SparseStayInAreaObjectionFunction : SpatialObjectionFunction {
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override Single InternalEvaluate() { return 0.1f; }
    /// <summary>
    ///
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override void InternalReset() {  }
  }
}
