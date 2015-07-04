﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbientSoundsTuner.SoundPack;

namespace AmbientSoundsTuner.SoundPatchers
{
    /// <summary>
    /// An abstract class that holds a generic implementation to patch sound instances.
    /// </summary>
    /// <typeparam name="T">The type of the sound ids.</typeparam>
    public abstract class SoundsInstancePatcher<T>
    {
        /// <summary>
        /// Gets the ids used by this patcher.
        /// </summary>
        public abstract T[] Ids { get; }

        private Dictionary<T, float> oldVolumes = new Dictionary<T, float>();

        /// <summary>
        /// Gets the backed up volumes.
        /// </summary>
        public Dictionary<T, float> OldVolumes
        {
            get { return this.oldVolumes; }
        }

        private Dictionary<T, SoundPackFile.Audio> oldSounds = new Dictionary<T, SoundPackFile.Audio>();

        /// <summary>
        /// Gets the backed up sounds.
        /// </summary>
        public Dictionary<T, SoundPackFile.Audio> OldSounds
        {
            get { return this.oldSounds; }
        }


        private int BackupAll(Func<T, bool> backupFunc)
        {
            int counter = 0;
            foreach (T key in this.Ids)
            {
                if (backupFunc(key))
                {
                    Mod.Log.Debug("Sound instance of '{0}' has been backed up", key);
                    counter++;
                }
                else
                {
                    Mod.Log.Debug("Sound instance of '{0}' has not been backed up", key);
                }
            }
            return counter;
        }

        private int PatchAll<V>(Func<T, V, bool> patchFunc, IDictionary<T, V> newItems)
        {
            int counter = 0;
            foreach (var newItem in newItems)
            {
                if (patchFunc(newItem.Key, newItem.Value))
                {
                    Mod.Log.Debug("Sound instance of '{0}' has been patched", newItem.Key);
                    counter++;
                }
                else
                {
                    Mod.Log.Debug("Sound instance of '{0}' has not been patched", newItem.Key);
                }
            }
            return counter;
        }


        /// <summary>
        /// Backs up the volumes.
        /// </summary>
        /// <returns>The number of succeeded operations.</returns>
        public virtual int BackupAllVolumes()
        {
            return this.BackupAll(this.BackupVolume);
        }

        /// <summary>
        /// Backs up a specific volume.
        /// </summary>
        /// <param name="id">The id of the volume to back up.</param>
        /// <returns>True if successful; false otherwise.</returns>
        public abstract bool BackupVolume(T id);

        /// <summary>
        /// Patches the volumes.
        /// </summary>
        /// <param name="newVolumes">The new volumes.</param>
        /// <returns>The number of succeeded operations.</returns>
        public virtual int PatchAllVolumes(IDictionary<T, float> newVolumes)
        {
            return this.PatchAll(this.PatchVolume, newVolumes);
        }

        /// <summary>
        /// Patches a specific volume.
        /// </summary>
        /// <param name="id">The id of the volume to patch.</param>
        /// <param name="newVolume">The new volume.</param>
        /// <returns>True if successful; false otherwise.</returns>
        public abstract bool PatchVolume(T id, float newVolume);

        /// <summary>
        /// Reverts the volumes to the default values.
        /// </summary>
        /// <returns>The number of succeeded operations.</returns>
        public virtual int RevertAllVolumes()
        {
            return this.PatchAllVolumes(this.OldVolumes);
        }


        /// <summary>
        /// Backs up the sounds.
        /// </summary>
        /// <returns>The number of succeeded operations.</returns>
        public virtual int BackupAllSounds()
        {
            return this.BackupAll(this.BackupSound);
        }

        /// <summary>
        /// Backs up a specific sound.
        /// </summary>
        /// <param name="id">The id of the sound to back up.</param>
        /// <returns>True if successful; false otherwise.</returns>
        public abstract bool BackupSound(T id);

        /// <summary>
        /// Patches the sounds.
        /// </summary>
        /// <param name="newSounds">The new sounds.</param>
        /// <returns>The number of succeeded operations.</returns>
        public virtual int PatchAllSounds(IDictionary<T, SoundPackFile.Audio> newSounds)
        {
            return this.PatchAll(this.PatchSound, newSounds);
        }

        /// <summary>
        /// Patches a specific sound.
        /// </summary>
        /// <param name="id">The id of the sound to patch.</param>
        /// <param name="newSound">The new sound.</param>
        /// <returns>True if successful; false otherwise.</returns>
        public abstract bool PatchSound(T id, SoundPackFile.Audio newSound);

        /// <summary>
        /// Reverts the sounds to the default values.
        /// </summary>
        /// <returns>The number of succeeded operations.</returns>
        public virtual int RevertAllSounds()
        {
            return this.PatchAllSounds(this.OldSounds);
        }
    }
}
