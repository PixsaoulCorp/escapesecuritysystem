using UnityEngine;
using UnityEngine.UI;

namespace Pixsaoul.Chat
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class EntryView : MonoBehaviour
    {
        [SerializeField] private Text _content;
        private Entry _data;

        /// <summary>
        /// Initializes the specified reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public void Initialize(Entry reference)
        {
            _data = reference;
            UpdateContent();
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        private void UpdateContent()
        {
            _content.text = _data.Sender.ToString() + ": " + _data.Content.ToString();
        }
    }
}