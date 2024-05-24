using Godot;
using System;
using System.Runtime.InteropServices;

public partial class ParticleRunner : Node2D
{

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("Click")){
			ParticleManager.instance.addOneShotParticle(0, GetGlobalMousePosition(), this);
		}
	}
	public void onSliderChanged(float value){
		// GD.Print("Value: " + value + " gravity: " + ParticleManager.instance.getParticle(0).ProcessMaterial.Get("gravity"));
		ParticleManager.instance.getParticle(0).ProcessMaterial.Set("gravity", new Vector3(0, value, 0));
	}
	public void onSliderChangedLifetime(float value){
		// GD.Print(ParticleManager.instance.getParticle(0).ProcessMaterial.Get("lifetime"));
		ParticleManager.instance.getParticle(0).Lifetime = value;
	}
}
