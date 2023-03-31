using Sgorey.Microloans.Infrastructure;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Sgorey.UIFramework.Runtime
{
	public class PanelManager : MonoBehaviour, IService
	{
		public Panel CurrentPanel => _currPanel;

		[SerializeField]
		private Panel _initialPanelPrefab;

		private Panel _currPanelPrefab;
		private Panel _prevPanelPrefab;
		private Panel _currPanel;

		private void Awake()
		{
			ServiceContainer.RegisterShared(this);

			if (_initialPanelPrefab)
			{
				OpenPanel(_initialPanelPrefab);
			}
		}

        private void OnDestroy()
        {
			ServiceContainer.UnregisterShared<PanelManager>();
        }

        public T OpenPanel<T>(T prefab) where T : Panel
		{
			Assert.IsNotNull(prefab);

			_prevPanelPrefab = _currPanelPrefab;
			_currPanelPrefab = prefab;

			if (_currPanel)
				Destroy(_currPanel.gameObject);

			T panel = CreatePanel(prefab);
			_currPanel = panel;

			return panel;
		}

		public bool TryBack(out Panel panel)
		{
			if (!_prevPanelPrefab)
			{
				panel = null;
				return false;
			}

			panel = OpenPanel(_prevPanelPrefab);
			return true;
		}

		public void Back(out Panel panel)
		{
			if (!_prevPanelPrefab)
			{
				throw new InvalidOperationException();
			}

			panel = OpenPanel(_prevPanelPrefab);
		}

		private T CreatePanel<T>(T prefab) where T : Panel
		{
			T panel = Instantiate(prefab, transform);
			panel.Init(this);
			return panel;
		}
	}
}
