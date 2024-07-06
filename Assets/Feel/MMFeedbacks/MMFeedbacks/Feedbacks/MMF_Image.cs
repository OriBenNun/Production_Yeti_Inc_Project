﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	/// <summary>
	/// This feedback will let you change the color of a target sprite renderer over time, and flip it on X or Y. You can also use it to command one or many MMSpriteRendererShakers.
	/// </summary>
	[AddComponentMenu("")]
	[FeedbackHelp("This feedback will let you change the color of a target Image over time. You can also use it to command one or many MMImageShakers.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks")]
	[FeedbackPath("UI/Image")]
	public class MMF_Image : MMF_Feedback
	{
		/// a static bool used to disable all feedbacks of this type at once
		public static bool FeedbackTypeAuthorized = true;
		/// sets the inspector color for this feedback
		#if UNITY_EDITOR
		public override Color FeedbackColor { get { return MMFeedbacksInspectorColors.UIColor; } }
		public override bool EvaluateRequiresSetup() { return (BoundImage == null); }
		public override string RequiredTargetText { get { return BoundImage != null ? BoundImage.name : "";  } }
		public override string RequiresSetupText { get { return "This feedback requires that a BoundImage be set to be able to work properly. You can set one below."; } }
		#endif

		/// the duration of this feedback is the duration of the Image, or 0 if instant
		public override float FeedbackDuration { get { return (Mode == Modes.Instant) ? 0f : ApplyTimeMultiplier(Duration); } set { Duration = value; } }
		public override bool HasChannel => true;
		public override bool HasAutomatedTargetAcquisition => true;
		protected override void AutomateTargetAcquisition() => BoundImage = FindAutomatedTarget<Image>();

		/// the possible modes for this feedback
		public enum Modes { OverTime, Instant }

		[MMFInspectorGroup("Image", true, 54, true)]
		/// the Image to affect when playing the feedback
		[Tooltip("the Image to affect when playing the feedback")]
		public Image BoundImage;
		/// whether the feedback should affect the Image instantly or over a period of time
		[Tooltip("whether the feedback should affect the Image instantly or over a period of time")]
		public Modes Mode = Modes.OverTime;
		/// how long the Image should change over time
		[Tooltip("how long the Image should change over time")]
		[MMFEnumCondition("Mode", (int)Modes.OverTime)]
		public float Duration = 0.2f;
		/// if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over
		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")] 
		public bool AllowAdditivePlays = false;
		/// whether or not to modify the color of the image
		[Tooltip("whether or not to modify the color of the image")]
		public bool ModifyColor = true;
		/// the colors to apply to the Image over time
		[Tooltip("the colors to apply to the Image over time")]
		[MMFEnumCondition("Mode", (int)Modes.OverTime)]
		public Gradient ColorOverTime;
		/// the color to move to in instant mode
		[Tooltip("the color to move to in instant mode")]
		[MMFEnumCondition("Mode", (int)Modes.Instant)]
		public Color InstantColor;
		/// whether or not that Image should be turned off on start
		[Tooltip("whether or not that Image should be turned off on start")]
		[FormerlySerializedAs("StartsOff")]
		public bool DisableOnInit = false;
		/// if this is true, the target will be enabled when this feedback gets played
		[Tooltip("if this is true, the target will be enabled when this feedback gets played")] 
		public bool EnableOnPlay = true;
		/// if this is true, the target disabled after the color over time change ends
		[Tooltip("if this is true, the target disabled after the color over time change ends")]
		public bool DisableOnSequenceEnd = false;
		/// if this is true, the target will be disabled when this feedbacks is stopped
		[Tooltip("if this is true, the target will be disabled when this feedbacks is stopped")] 
		public bool DisableOnStop = false;

		protected Coroutine _coroutine;
		protected Color _initialColor;

		/// <summary>
		/// On init we turn the Image off if needed
		/// </summary>
		/// <param name="owner"></param>
		protected override void CustomInitialization(MMF_Player owner)
		{
			base.CustomInitialization(owner);

			if (Active)
			{
				if (DisableOnInit)
				{
					Turn(false);
				}
			}
		}

		/// <summary>
		/// On Play we turn our Image on and start an over time coroutine if needed
		/// </summary>
		/// <param name="position"></param>
		/// <param name="feedbacksIntensity"></param>
		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
		{
			if (!Active || !FeedbackTypeAuthorized)
			{
				return;
			}
        
			_initialColor = BoundImage.color;
			if (EnableOnPlay)
			{
				Turn(true);	
			}
			switch (Mode)
			{
				case Modes.Instant:
					if (ModifyColor)
					{
						BoundImage.color = InstantColor;
					}
					break;
				case Modes.OverTime:
					if (!AllowAdditivePlays && (_coroutine != null))
					{
						return;
					}
					_coroutine = Owner.StartCoroutine(ImageSequence());
					break;
			}
		}

		/// <summary>
		/// This coroutine will modify the values on the Image
		/// </summary>
		/// <returns></returns>
		protected virtual IEnumerator ImageSequence()
		{
			float journey = NormalPlayDirection ? 0f : FeedbackDuration;

			IsPlaying = true;
			while ((journey >= 0) && (journey <= FeedbackDuration) && (FeedbackDuration > 0))
			{
				float remappedTime = MMFeedbacksHelpers.Remap(journey, 0f, FeedbackDuration, 0f, 1f);

				SetImageValues(remappedTime);

				journey += NormalPlayDirection ? FeedbackDeltaTime : -FeedbackDeltaTime;
				yield return null;
			}
			SetImageValues(FinalNormalizedTime);
			if (DisableOnSequenceEnd)
			{
				Turn(false);
			}
			IsPlaying = false;
			_coroutine = null;
			yield return null;
		}

		/// <summary>
		/// Sets the various values on the sprite renderer on a specified time (between 0 and 1)
		/// </summary>
		/// <param name="time"></param>
		protected virtual void SetImageValues(float time)
		{
			if (ModifyColor)
			{
				BoundImage.color = ColorOverTime.Evaluate(time);
			}
		}

		/// <summary>
		/// Turns the sprite renderer off on stop
		/// </summary>
		/// <param name="position"></param>
		/// <param name="feedbacksIntensity"></param>
		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1)
		{
			if (!Active || !FeedbackTypeAuthorized)
			{
				return;
			}
			IsPlaying = false;
			base.CustomStopFeedback(position, feedbacksIntensity);
			if (Active && DisableOnStop)
			{
				Turn(false);    
			}

			if (_coroutine != null)
			{
				Owner.StopCoroutine(_coroutine);	
			}
			
			_coroutine = null;
		}

		/// <summary>
		/// Turns the sprite renderer on or off
		/// </summary>
		/// <param name="status"></param>
		protected virtual void Turn(bool status)
		{
			BoundImage.gameObject.SetActive(status);
			BoundImage.enabled = status;
		}
		
		/// <summary>
		/// On restore, we restore our initial state
		/// </summary>
		protected override void CustomRestoreInitialValues()
		{
			if (!Active || !FeedbackTypeAuthorized)
			{
				return;
			}
			BoundImage.color = _initialColor;
		}
	}
}