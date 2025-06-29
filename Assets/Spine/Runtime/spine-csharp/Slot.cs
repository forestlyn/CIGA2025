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

#if UNITY_5_3_OR_NEWER
#define IS_UNITY
#endif

using System;

namespace Spine {
#if IS_UNITY
	using Color = UnityEngine.Color;
#endif

	/// <summary>
	/// Stores a slot's current pose. Slots organize attachments for <see cref="Skeleton.DrawOrder"/> purposes and provide a place to store
	/// state for an attachment.State cannot be stored in an attachment itself because attachments are stateless and may be shared
	/// across multiple skeletons.
	/// </summary>
	public class Slot : Posed<SlotData, SlotPose, SlotPose> {
		internal readonly Skeleton skeleton;
		internal readonly Bone bone;
		internal int attachmentState;

		public Slot (SlotData data, Skeleton skeleton)
			: base(data, new SlotPose(), new SlotPose()) {
			if (skeleton == null) throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			this.skeleton = skeleton;
			bone = skeleton.bones.Items[data.boneData.index];
			if (data.setup.GetDarkColor().HasValue) {
				pose.SetDarkColor(new Color());
				constrained.SetDarkColor(new Color());
			}
			SetupPose();
		}

		/// <summary>Copy constructor.</summary>
		public Slot (Slot slot, Bone bone, Skeleton skeleton)
			: base(slot.data, new SlotPose(), new SlotPose()) {
			if (bone == null) throw new ArgumentNullException("bone", "bone cannot be null.");
			if (skeleton == null) throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			this.bone = bone;
			this.skeleton = skeleton;
			if (data.setup.GetDarkColor().HasValue) {
				pose.SetDarkColor(new Color());
				constrained.SetDarkColor(new Color());
			}
			pose.Set(slot.pose);
		}

		/// <summary>The bone this slot belongs to.</summary>
		public Bone Bone { get { return bone; } }

		/// <summary>Sets this slot to the setup pose.</summary>
		override public void SetupPose () {
			pose.SetColor(data.setup.GetColor());
			if (pose.GetDarkColor().HasValue) pose.SetDarkColor(data.setup.GetDarkColor());
			pose.sequenceIndex = data.setup.sequenceIndex;
			if (data.attachmentName == null)
				pose.Attachment = null;
			else {
				pose.attachment = null;
				pose.Attachment = skeleton.GetAttachment(data.index, data.attachmentName);
			}
		}
	}
}
