using Godot;
using System;

public partial class ParticleManager : Node
{
	[Export] GpuParticles2D[] gpuParticles2D;

	public static ParticleManager instance;
    public override void _Ready()
    {
        instance = this;
    }

    public void addOneShotParticle(int id, Vector2 position, Node obj){
		GpuParticles2D g = (GpuParticles2D)gpuParticles2D[id].Duplicate();
		g.OneShot = true;
		g.Position = position;
		obj.AddChild(g);
		g.Emitting = true;
		g.Connect("finished", new Callable(g, MethodName.QueueFree));
	}

	public GpuParticles2D getParticle(int id){
		return gpuParticles2D[id];
	}
}
