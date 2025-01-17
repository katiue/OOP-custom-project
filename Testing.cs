﻿using SplashKitSDK;
using System;

namespace OOP_custom_project
{
    public class Testing
    {
        private readonly Bitmap _mapImage;
        private float _zoom = 1.0f;
        private float _offsetY, _offsetX = 0;
        public Testing()
        {
            _mapImage = new Bitmap("Map", @"D:\OOP-custom-project\Image\map.webp");
        }

        public void MakeMapObjectLayer()
        {
            Window window = new("Game screen", 1090, 820);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                SplashKit.DrawBitmap(_mapImage, _offsetX, _offsetY, SplashKit.OptionScaleBmp(_zoom, _zoom));

                Define difineZone = new();
                foreach (var zone in difineZone.mineralZones)
                {
                    SplashKit.FillRectangle(zone._mineral.Color, zone.startX + _offsetX, zone.startY + _offsetY, zone.endX - zone.startX, zone.endY - zone.startY);
                }

                // Handle input for zooming
                Vector2D scroll = SplashKit.MouseWheelScroll();
                // Calculate the new zoom level0.0f < 3f))
                _zoom += (float)scroll.Y / 10.0f;

                // Handle input for panning
                Vector2D pos = SplashKit.MouseMovement();
                if (SplashKit.MouseDown(MouseButton.LeftButton))
                {
                    _offsetX += (float)pos.X;
                    _offsetY += (float)pos.Y;
                }
                if(SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Console.WriteLine("Mouse clicked at: " + SplashKit.MouseX() + " " + SplashKit.MouseY());
                }   

                SplashKit.RefreshScreen(30);
            } while (!window.CloseRequested);
        }
    }
}
