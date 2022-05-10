using Normal.Realtime;
using Normal.Realtime.Serialization;

namespace ETS.Realtime
{
    [RealtimeModel]
    public partial class MultipleButtonsModel
    {
        [RealtimeProperty(1, true)] private bool _buttonAPressed;
        [RealtimeProperty(2, true)] private bool _buttonBPressed;
    }
}

/* ----- Begin Normal Autogenerated Code ----- */
namespace ETS.Realtime {
    public partial class MultipleButtonsModel : RealtimeModel {
        public bool buttonAPressed {
            get {
                return _buttonAPressedProperty.value;
            }
            set {
                if (_buttonAPressedProperty.value == value) return;
                _buttonAPressedProperty.value = value;
                InvalidateReliableLength();
            }
        }
        
        public bool buttonBPressed {
            get {
                return _buttonBPressedProperty.value;
            }
            set {
                if (_buttonBPressedProperty.value == value) return;
                _buttonBPressedProperty.value = value;
                InvalidateReliableLength();
            }
        }
        
        public enum PropertyID : uint {
            ButtonAPressed = 1,
            ButtonBPressed = 2,
        }
        
        #region Properties
        
        private ReliableProperty<bool> _buttonAPressedProperty;
        
        private ReliableProperty<bool> _buttonBPressedProperty;
        
        #endregion
        
        public MultipleButtonsModel() : base(null) {
            _buttonAPressedProperty = new ReliableProperty<bool>(1, _buttonAPressed);
            _buttonBPressedProperty = new ReliableProperty<bool>(2, _buttonBPressed);
        }
        
        protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
            _buttonAPressedProperty.UnsubscribeCallback();
            _buttonBPressedProperty.UnsubscribeCallback();
        }
        
        protected override int WriteLength(StreamContext context) {
            var length = 0;
            length += _buttonAPressedProperty.WriteLength(context);
            length += _buttonBPressedProperty.WriteLength(context);
            return length;
        }
        
        protected override void Write(WriteStream stream, StreamContext context) {
            var writes = false;
            writes |= _buttonAPressedProperty.Write(stream, context);
            writes |= _buttonBPressedProperty.Write(stream, context);
            if (writes) InvalidateContextLength(context);
        }
        
        protected override void Read(ReadStream stream, StreamContext context) {
            var anyPropertiesChanged = false;
            while (stream.ReadNextPropertyID(out uint propertyID)) {
                var changed = false;
                switch (propertyID) {
                    case (uint) PropertyID.ButtonAPressed: {
                        changed = _buttonAPressedProperty.Read(stream, context);
                        break;
                    }
                    case (uint) PropertyID.ButtonBPressed: {
                        changed = _buttonBPressedProperty.Read(stream, context);
                        break;
                    }
                    default: {
                        stream.SkipProperty();
                        break;
                    }
                }
                anyPropertiesChanged |= changed;
            }
            if (anyPropertiesChanged) {
                UpdateBackingFields();
            }
        }
        
        private void UpdateBackingFields() {
            _buttonAPressed = buttonAPressed;
            _buttonBPressed = buttonBPressed;
        }
        
    }
}
/* ----- End Normal Autogenerated Code ----- */
