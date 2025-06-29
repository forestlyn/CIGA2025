/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated April 5, 2025. Replaces all prior versions.
 *
 * Copyright (c) 2013-2025, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
 * BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using System;

namespace Spine {
	public interface IPosedData {
		bool SkinRequired { get; }
	}

	/// <summary>
	/// The base class for all constrained datas.
	/// </summary>
	public class PosedData<P> : IPosedData
		where P : IPose<P> {

		internal readonly string name;
		internal readonly P setup;
		internal bool skinRequired;

		public PosedData (string name, P setup) {
			if (name == null) throw new ArgumentNullException("name", "name cannot be null.");
			this.name = name;
			this.setup = setup;
		}

		///<summary>The constraint's name, which is unique across all constraints in the skeleton of the same type.</summary>
		public string Name { get { return name; } }

		public P GetSetupPose () {
			return setup;
		}

		/// <summary>
		/// When true, <see cref="Skeleton.UpdateWorldTransform(Physics)"/> only updates this constraint if the <see cref="Skeleton.Skin"/>
		/// contains this constraint.
		/// </summary>
		/// <seealso cref="Skin.Constraints"/>
		public bool SkinRequired {
			get { return skinRequired; }
			set { skinRequired = value; }
		}

		override public string ToString () {
			return name;
		}
	}
}
