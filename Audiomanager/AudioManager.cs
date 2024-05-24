using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class AudioManager : Node
{
	[Export] public Sound[] sounds;
	private Dictionary<string, Sound> dict = new Dictionary<string, Sound>();
	[Export] public static AudioManager instance;
    public override void _Ready()
    {
        if (instance == null){
			instance = this;
		}
		else{
			this.QueueFree();
		}
		foreach (Sound s in sounds){
			dict.Add(s.name, s);
		}
    }
    public void PlaySFX(Node from, string sound){
		var player = new AudioStreamPlayer();
		Sound s;
		s = dict[sound];
		player.Stream = s.stream;
		player.VolumeDb = s.volume;
		player.Connect("finished", new Callable(player, MethodName.QueueFree));
		from.AddChild(player);
		player.Play();
	}
}
