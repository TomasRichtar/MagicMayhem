using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RichiGames
{
    [RequireComponent(typeof(Animator))]
    public class RewindableAnimation : RewindableObject
    {
        private Animator _animator;

        private int savedStateHash;
        private float savedNormalizedTime;

        protected override void Start()
        {
            base.Start();

            _animator = GetComponent<Animator>();
        }
        public override void StopTime()
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            savedStateHash = currentState.fullPathHash;
            savedNormalizedTime = currentState.normalizedTime % 1;

            _animator.speed = 0;
        }

        public override void ContinueTime()
        {
            _animator.Play(savedStateHash, 0, savedNormalizedTime);
            _animator.speed = 1;
        }

        public override void RecordStep()
        {
            if (_rewindData.Count > Mathf.Round(TimeController.Instance.RewindStorageLimit / Time.fixedDeltaTime))
            {
                _rewindData.RemoveLast();
            }

            RewindData rewindData = new RewindData
            (
                _animator.GetCurrentAnimatorStateInfo(0).normalizedTime,
                _animator.GetCurrentAnimatorStateInfo(0),
                GetAnimatorParameters()
            );
            _rewindData.AddFirst(rewindData);
        }

        private Dictionary<string, float> GetAnimatorParameters()
        {
            Dictionary<string, float> parameters = new Dictionary<string, float>();

            foreach (AnimatorControllerParameter parameter in _animator.parameters)
            {
                switch (parameter.type)
                {
                    case AnimatorControllerParameterType.Float:
                        parameters[parameter.name] = _animator.GetFloat(parameter.name);
                        break;
                    case AnimatorControllerParameterType.Int:
                        parameters[parameter.name] = _animator.GetInteger(parameter.name);
                        break;
                    case AnimatorControllerParameterType.Bool:
                        parameters[parameter.name] = _animator.GetBool(parameter.name) ? 1f : 0f;
                        break;
                }
            }

            return parameters;
        }

        public override void RewindStep()
        {
            if (_rewindData.Count == 0) return;

            RewindData rewindData = _rewindData.First.Value;

            RestoreAnimatorState(rewindData);

            _rewindData.RemoveFirst();
        }

        private void RestoreAnimatorState(RewindData data)
        {
            foreach (var parameter in data.Parameters)
            {
                if (_animator.parameters.Any(p => p.name == parameter.Key))
                {
                    var param = _animator.parameters.First(p => p.name == parameter.Key);
                    if (param.type == AnimatorControllerParameterType.Float && _animator.GetFloat(parameter.Key) != parameter.Value)
                    {
                        _animator.SetFloat(parameter.Key, parameter.Value);
                    }
                    else if (param.type == AnimatorControllerParameterType.Int && _animator.GetInteger(parameter.Key) != (int)parameter.Value)
                    {
                        _animator.SetInteger(parameter.Key, (int)parameter.Value);
                    }
                    else if (param.type == AnimatorControllerParameterType.Bool && _animator.GetBool(parameter.Key) != (parameter.Value > 0f))
                    {
                        _animator.SetBool(parameter.Key, parameter.Value > 0f);
                    }
                }
            }

            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

            if (currentState.shortNameHash != data.StateInfo.shortNameHash
                || Mathf.Abs(currentState.normalizedTime - data.AnimatorTime) > 0.01f)
            {
                _animator.Play(data.StateInfo.shortNameHash, 0, data.AnimatorTime);
            }
        }

    }
}
