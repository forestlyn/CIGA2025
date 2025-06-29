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
	/// <summary>
	/// Stores the current pose for a slider. 
	/// </summary>
	public class Slider : Constraint<Slider, SliderData, SliderPose> {
		static private readonly float[] offsets = new float[6];
		internal Bone bone;

		public Slider (SliderData data, Skeleton skeleton)
			: base(data, new SliderPose(), new SliderPose()) {
			if (skeleton == null) throw new ArgumentNullException("skeleton", "skeleton cannot be null.");

			if (data.bone != null) bone = skeleton.bones.Items[data.bone.index];
		}

		override public IConstraint Copy (Skeleton skeleton) {
			var copy = new Slider(data, skeleton);
			copy.pose.Set(pose);
			return copy;
		}

		override public void Update (Skeleton skeleton, Physics physics) {
			SliderPose p = applied;
			if (p.mix == 0) return;

			Animation animation = data.animation;
			if (bone != null) {
				if (!bone.active) return;
				if (data.local) bone.applied.ValidateLocalTransform(skeleton);
				p.time = data.offset
					+ (data.property.Value(skeleton, bone.applied, data.local, offsets) - data.property.offset) * data.scale;
				if (data.loop)
					p.time = animation.duration + (p.time % animation.duration);
				else
					p.time = Math.Max(0, p.time);
			}

			Bone[] bones = skeleton.bones.Items;
			int[] indices = animation.bones.Items;
			for (int i = 0, n = animation.bones.Count; i < n; i++)
				bones[indices[i]].applied.ModifyLocal(skeleton);

			animation.Apply(skeleton, p.time, p.time, data.loop, null, p.mix, data.additive ? MixBlend.Add : MixBlend.Replace,
				MixDirection.In, true);
		}

		override public void Sort (Skeleton skeleton) {
			if (bone != null && !data.local) skeleton.SortBone(bone);
			skeleton.updateCache.Add(this);

			Timeline[] timelines = data.animation.timelines.Items;
			Bone[] bones = skeleton.bones.Items;
			Slot[] slots = skeleton.slots.Items;
			IConstraint[] constraints = skeleton.constraints.Items;
			PhysicsConstraint[] physics = skeleton.physics.Items;
			int physicsCount = skeleton.physics.Count;
			for (int i = 0, n = data.animation.timelines.Count; i < n; i++) {
				Timeline t = timelines[i];
				IBoneTimeline boneTimeline = t as IBoneTimeline;
				if (boneTimeline != null) {
					Bone bone = bones[boneTimeline.BoneIndex];
					bone.sorted = false;
					skeleton.SortReset(bone.children);
					skeleton.Constrained(bone);
				} else if (t as ISlotTimeline != null) {
					ISlotTimeline timeline = (ISlotTimeline)t;
					skeleton.Constrained(slots[timeline.SlotIndex]);
				} else if (t as PhysicsConstraintTimeline != null) {
					PhysicsConstraintTimeline timeline = (PhysicsConstraintTimeline)t;
					if (timeline.constraintIndex == -1) {
						for (int ii = 0; ii < physicsCount; ii++)
							skeleton.Constrained(physics[ii]);
					} else
						skeleton.Constrained((IPosedInternal)constraints[timeline.constraintIndex]);
				} else if (t as IConstraintTimeline != null) {
					IConstraintTimeline timeline = (IConstraintTimeline)t;
					skeleton.Constrained((IPosedInternal)constraints[timeline.ConstraintIndex]);
				}
			}
		}

		public Bone Bone { get { return bone; } set { bone = value; } }
	}
}
