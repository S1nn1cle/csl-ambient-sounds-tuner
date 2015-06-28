﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonShared.Extensions;

namespace AmbientSoundsTuner
{
    /// <summary>
    /// A class that can patch instances of effect sounds.
    /// </summary>
    public class SoundsInstanceEffectsPatcher : SoundsInstancePatcher<string>
    {
        public const string ID_AIRCRAFT_MOVEMENT = "Aircraft Movement";
        public const string ID_AMBULANCE_SIREN = "Ambulance Siren";
        public const string ID_FIRE_TRUCK_SIREN = "Fire Truck Siren";
        public const string ID_LARGE_CAR_MOVEMENT = "Large Car Movement";
        public const string ID_METRO_MOVEMENT = "Metro Movement";
        public const string ID_POLICE_CAR_SIREN = "Police Car Siren";
        public const string ID_SMALL_CAR_MOVEMENT = "Small Car Movement";
        public const string ID_TRAIN_MOVEMENT = "Train Movement";
        public const string ID_TRANSPORT_ARRIVE = "Transport Arrive";

        public SoundsInstanceEffectsPatcher()
            : base()
        {
            this.DefaultVolumes.Add(ID_AIRCRAFT_MOVEMENT, 0.5f);
            this.DefaultVolumes.Add(ID_AMBULANCE_SIREN, 1);
            this.DefaultVolumes.Add(ID_FIRE_TRUCK_SIREN, 3);
            this.DefaultVolumes.Add(ID_LARGE_CAR_MOVEMENT, 1.5f);
            this.DefaultVolumes.Add(ID_METRO_MOVEMENT, 0.5f);
            this.DefaultVolumes.Add(ID_POLICE_CAR_SIREN, 1);
            this.DefaultVolumes.Add(ID_SMALL_CAR_MOVEMENT, 1.5f);
            this.DefaultVolumes.Add(ID_TRAIN_MOVEMENT, 0.5f);
            this.DefaultVolumes.Add(ID_TRANSPORT_ARRIVE, 1);
        }

        public override bool BackupVolume(string id)
        {
            SoundEffect soundEffect = SoundsCollection.Effects[id];
            if (soundEffect != null)
            {
                float? volume = SoundsPatcher.GetVolume(soundEffect);
                if (volume.HasValue)
                {
                    this.DefaultVolumes[id] = volume.Value;
                    return true;
                }
            }
            return false;
        }

        public override bool PatchVolume(string id, float newVolume)
        {
            SoundEffect soundEffect = SoundsCollection.Effects[id];
            if (soundEffect != null)
            {
                return SoundsPatcher.SetVolume(soundEffect, newVolume);
            }
            return false;
        }
    }
}
