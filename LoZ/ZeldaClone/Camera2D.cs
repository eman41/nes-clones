// -----------------------------------------------------------------------
// Camera2D.cs
// Author: Eric S. Policaro
// Standard orthographic camera.
// Features:
//      -- Locked to viewport
//      -- Can be locked in any direction
//      -- Contains a queue that can be used to hold the camera
//         after it passes a certain point
// Code basis and inspiration from:
// http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
// -----------------------------------------------------------------------

using System;
using Microsoft.Xna.Framework;

namespace ZeldaClone
{
    /// <summary>
    /// Class for a standard orthographic camera.
    /// </summary>
    public class Camera2D
    {
        /// <summary>
        /// Creates a new camera.
        /// </summary>
        public Camera2D()
        {
            _zoom = 1.0f;
            Rotation = 0.0f;
            _position = Vector2.Zero;
            Parallax = Vector2.One;
        }

        /// <summary>
        /// Create a new camera with ths specified dimensions.
        /// </summary>
        /// <param name="viewWidth">Width of the viewport</param>
        /// <param name="viewHeight">Height of the viewport</param>
        public Camera2D(float viewWidth, float viewHeight)
            : this()
        {
            _viewPortWidth = viewWidth;
            _viewPortHeight = viewHeight;
            Origin = new Vector2(_viewPortWidth / 2.0f, _viewPortHeight / 2.0f);
        }
        
        public float Rotation { get; set; }
        public Rectangle? Limits { get; set; }
        public Vector2 Parallax { get; set; }
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Gets or sets the zoom level.
        /// A negative zoom will flip the image.
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < 0.1f)
                    _zoom = 0.1f;
                ValidateZoom();
            }
        }

        /// <summary>
        /// Gets the matrix that creates the projection for the camera.
        /// </summary>
        public Matrix Transform
        {
            get
            {
                Vector3 translation = new Vector3(_viewPortWidth * 0.5f, _viewPortHeight * 0.5f, 0);
                Vector3 zoomVector = new Vector3(Zoom, Zoom, 1);
                return
                    Matrix.CreateTranslation(
                        new Vector3(-_position * Parallax, 0))
                        * Matrix.CreateRotationZ(Rotation)
                        * Matrix.CreateScale(zoomVector)
                        * Matrix.CreateTranslation(translation);
            }
        }

        /// <summary>
        /// Get or set the camera position taking into account the camera lock.
        /// </summary>
        public Vector2 FocusVector
        {
            get { return _position; }
            set
            {
                _position = value;
            }
        }

        public void Update(GameTime time)
        {
            if (IsMoving)
            {
                Vector2 delta = (_movePerSec * (float)time.ElapsedGameTime.TotalSeconds);
                Vector2 remaining = _moveTarget - _position;

                float dX = Math.Abs(remaining.X) < Math.Abs(delta.X) ? remaining.X : delta.X;
                float dY = Math.Abs(remaining.Y) < Math.Abs(delta.Y) ? remaining.Y : delta.Y;
                
                _position += new Vector2(dX, dY);
                if (_position == _moveTarget)
                {
                    _position = _moveTarget;
                    IsMoving = false;
                }
            }
        }

        public bool IsMoving { get; private set; }

        public void MoveTo(Vector2 pos, double time = 1.0)
        {
            _moveTarget = pos;
            _moveTime = 1.0;
            _movePerSec = new Vector2
            {
                X = (_moveTarget.X - _position.X) / (float)_moveTime,
                Y = (_moveTarget.Y - _position.Y) / (float)_moveTime
            };

            IsMoving = true;
        }

        private Vector2 _moveTarget;
        private Vector2 _movePerSec;
        private double _moveTime;

        private void ValidateZoom()
        {
            if (Limits.HasValue)
            {
                float minZoomX = _viewPortWidth / Limits.Value.Width;
                float minZoomY = _viewPortHeight / Limits.Value.Height;
                _zoom = MathHelper.Max(_zoom, MathHelper.Max(minZoomX, minZoomY));
            }
        }

        private Vector2 _position;
        private float _zoom;
        private float _viewPortWidth;
        private float _viewPortHeight;
    }
}