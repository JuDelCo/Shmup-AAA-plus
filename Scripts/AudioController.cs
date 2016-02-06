using System.Collections.Generic;
using UnityEngine;

public static class AudioController
{
	static Dictionary<string, SfxrSynth> _cachedSounds;
	
	static Dictionary<string, SfxrSynth> cachedSounds
	{
        get
        {
            if (_cachedSounds == null)
            	_cachedSounds = new Dictionary<string, SfxrSynth>();
            return _cachedSounds;
        }
    }
	
	static SfxrSynth GetCachedSound(string name)
	{
		if(cachedSounds.ContainsKey(name))
		{
			return cachedSounds[name];
		}
		
		return null;
	}
	
	static SfxrSynth SetCachedSound(string name, string parameters)
	{
		cachedSounds[name] = new SfxrSynth();
		cachedSounds[name].parameters.SetSettingsString(parameters);
		
		return cachedSounds[name];
	}
	
	static public void PlayExplosion()
	{
		//var sound = new SfxrSynth();
		//sound.parameters.SetSettingsString("3,.257,,.228,.6585,.2589,.3," + Random.Range(0.065f, 0.080f) + ",,,,,,,,,-.3726,.8605,,,,,,,,1,,,,,,");
		//sound.Play();
		
		AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sfx/Explosion"), Vector3.zero); return;
	}
	
	static public void PlayBigExplosion()
	{
		//if(GetCachedSound("big_explosion") == null)
		{
			//var sound = SetCachedSound("big_explosion", "3,.268,,.275,.307,.446,.3,.101,,.0449,,,,,,,.4173,.7989,,,,,.7409,.545,-.2199,1,,,,,,");
			//sound.CacheSound(sound.Play);
			//return;
		}
		
		//GetCachedSound("big_explosion").Play();
		
		AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sfx/BigExplosion"), Vector3.zero); return;
	}
	
	static public void PlayBigShoot()
	{
		//if(GetCachedSound("big_shoot") == null)
		{
			//var sound = SetCachedSound("big_shoot", "1,.06,,.175,.039,.1204,.3,.63,.0753,-.419,,,,,,,,,,,.8361,-.0807,,.1149,-.0444,1,,,,,,");
			//sound.CacheSound(sound.Play);
			//return;
		}
		
		//GetCachedSound("big_shoot").Play();
		
		// TODO: Fix Play only ONCE per frame
		
		AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sfx/BigShoot"), Vector3.zero); return;
	}
	
	static public void PlaySmallShoot()
	{
		//if(GetCachedSound("small_shoot") == null)
		{
			//var sound = SetCachedSound("small_shoot", "1,.043,,.175,.039,.1204,.3,.833,.0753,-.419,,,,,,,,,,,.8361,-.0807,,.1149,-.0444,1,,,,,,");
			//sound.CacheSound(sound.Play);
			//return;
		}
		
		//GetCachedSound("small_shoot").Play();
		
		AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sfx/SmallShoot"), Vector3.zero); return;
	}
	
	static public void PlayHit()
	{
		//if(GetCachedSound("hit") == null)
		{
			//var sound = SetCachedSound("hit", "0,.123,,.0428,,.2001,.3,.4997,,-.5913,,,,,,,,,,,.1192,,,,,1,,,.2435,,,");
			//sound.CacheSound(sound.Play);
			//return;
		}
		
		//GetCachedSound("hit").Play();
		
		AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sfx/Hit"), Vector3.zero); return;
	}
	
	static public void PlayShieldEffect()
	{
		//if(GetCachedSound("shield") == null)
		{
			//var sound = SetCachedSound("shield", "1,.046,,.0987,,.1025,.3,.3522,,.1544,,.1453,.196,,,,,,,,,,,,,1,,,,,,");
			//sound.CacheSound(sound.Play);
			//return;
		}
		
		//GetCachedSound("shield").Play();
		
		AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sfx/ShieldEffect"), Vector3.zero); return;
	}
}
