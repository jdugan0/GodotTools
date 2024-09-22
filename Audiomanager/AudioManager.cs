using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public partial class AudioManager : Node
{
	[Export] public Sound[] sounds;
	private Dictionary<string, Sound> dict = new Dictionary<string, Sound>();
	[Export] public static AudioManager instance;
	public List<KeyValuePair<string, AudioStreamPlayer>> playing = new List<KeyValuePair<string, AudioStreamPlayer>>();

	public override void _Ready()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			this.QueueFree();
		}
		foreach (Sound s in sounds)
		{
			dict.Add(s.name, s);
		}
	}
	public AudioStreamPlayer PlaySFX(Node from, string sound, float time)
	{
		var player = new AudioStreamPlayer();
		player.ProcessMode = Node.ProcessModeEnum.Always;
		Sound s;
		s = dict[sound];
		player.Stream = s.stream;
		player.VolumeDb = s.volume;
		// playing.Add(s, player);
		player.Finished += () => playing.Remove(new KeyValuePair<string, AudioStreamPlayer>(s.name, player));
		player.Finished += () => player.QueueFree();
		playing.Add(new KeyValuePair<string, AudioStreamPlayer>(s.name, player));
		// player.Finished += ()=>playing.Remove(s);
		from.AddChild(player);
		player.Play(time);
		return player;
	}
	public AudioStreamPlayer PlaySFX(Node from, string sound)
	{
		return PlaySFX(from, sound, 0);
	}
	public float CancelSFX(string sound)
	{
		if (isPlaying(sound))
		{
			KeyValuePair<string, AudioStreamPlayer> p = new KeyValuePair<string, AudioStreamPlayer>();
			foreach (KeyValuePair<string, AudioStreamPlayer> pair in playing)
			{
				if (pair.Key == sound)
				{
					p = pair;
					break;
				}
			}
			float time=p.Value.GetPlaybackPosition();
			
			playing.Remove(p);
			p.Value.QueueFree();
			return time;
		}
		return -1;
	}
	public bool isPlaying(string sound)
	{
		foreach (KeyValuePair<string, AudioStreamPlayer> pair in playing)
		{
			if (pair.Key == sound)
			{
				return true;
			}
		}
		return false;
	}

}
