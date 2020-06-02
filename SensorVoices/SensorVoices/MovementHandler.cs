using Java.Util;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace SensorVoices
{
    public class MovementHandler
    {

        public enum MovementType { START, END, STATIC, LINEAR, ROLLING, CHOCK, OTHER, NULL }
        private MovementType currentMovement;

        public MovementType GetMovement { get => currentMovement; }

        private bool isPosed;
        private bool isMoving;

        private Stopwatch staticTime;
        private Stopwatch linearTime;
        private Stopwatch rollingTime;

        private float lastX;
        private float lastY;
        private float lastZ;

        private double lastVector;

        public MovementHandler()
        {
            this.isPosed = false;
            this.currentMovement = MovementType.NULL;

            this.staticTime = new Stopwatch();
            this.linearTime = new Stopwatch();
            this.rollingTime = new Stopwatch();
        }

        internal void AccelerationChanged(float newX, float newY, float newZ)
        {
            if (!isPosed)
            {
                this.lastX = newX * newX;
                this.lastY = newY * newY;
                this.lastZ = newZ * newZ;
                this.isPosed = true;
            }
            float vector = newX * newX - this.lastX + newY * newY - this.lastY + newZ * newZ - this.lastZ;
            HandleMovement((vector <= 0) ? 0 : Math.Sqrt(vector));
        }

        private void HandleMovement(double vector)
        {
            if (!this.isPosed)
            {
                this.currentMovement = MovementType.NULL;
                this.isMoving = false;
            }
            else if (!this.isMoving && vector > 0.3)
            {
                this.currentMovement = MovementType.START;
                this.isMoving = true;
            }
            else if (!this.isMoving) this.currentMovement = MovementType.STATIC;
            else if (Math.Abs(lastVector - vector) > 0.6 && vector < 0.2)
            {
                this.currentMovement = MovementType.CHOCK;
                this.isMoving = true;
            }
            else if (Math.Abs(lastVector - vector) > 0.6 && vector < 0.2)
            {
                if (rollingTime.ElapsedMilliseconds > 300) this.currentMovement = MovementType.ROLLING;
                else this.currentMovement = MovementType.OTHER;
                if (!rollingTime.IsRunning) rollingTime.Start();
                this.isMoving = true;
            }
            else if (Math.Abs(lastVector - vector) < 0.2 && vector > 0.2)
            {
                if (linearTime.ElapsedMilliseconds > 5000) this.currentMovement = MovementType.LINEAR;
                else this.currentMovement = MovementType.OTHER;
                if (!linearTime.IsRunning) this.linearTime.Start();
                this.isMoving = true;
            }
            else if (vector < 0.2)
            {
                if (staticTime.ElapsedMilliseconds > 4000 && this.isMoving)
                {
                    this.currentMovement = MovementType.END;
                    this.isMoving = false;
                }
                else this.currentMovement = MovementType.NULL;
                if (!staticTime.IsRunning) staticTime.Start();
            }
            else this.currentMovement = MovementType.OTHER;
            this.lastVector = vector;

            TimeManager();
        }

        private void TimeManager()
        {
            if (this.currentMovement == MovementType.ROLLING || this.currentMovement == MovementType.CHOCK)
            {
                this.linearTime.Stop();
                this.linearTime.Reset();
            }
            if (this.linearTime.ElapsedMilliseconds > 6000)
            {
                this.linearTime.Stop();
                this.linearTime.Restart();
            }
            if (this.currentMovement == MovementType.LINEAR || this.currentMovement == MovementType.CHOCK)
            {
                this.rollingTime.Stop();
                this.rollingTime.Reset();
            }
            if (this.rollingTime.ElapsedMilliseconds > 500)
            {
                this.rollingTime.Stop();
                this.rollingTime.Restart();
            }
            if (this.currentMovement != MovementType.NULL)
            {
                this.staticTime.Stop();
                this.staticTime.Reset();
            } 
            if (!this.isMoving)
            {
                this.staticTime.Stop();
                this.staticTime.Reset();
                this.linearTime.Stop();
                this.linearTime.Reset();
                this.rollingTime.Stop();
                this.rollingTime.Reset();
            }
        }
    }
}