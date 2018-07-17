/*{
	"DESCRIPTION": "voronoi",
	"CREDIT": "shadertoy.com/user/aytekaman",
	"CATEGORIES": [
		"mind expansion"
	],
	"INPUTS": [
		{
			"NAME": "iChannel0",
			"TYPE": "image"
		},
      	],
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);

#define NUM_POINTS 1024

#define SEED 3

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    int random = SEED;
    
    int a = 1103515245;
    int c = 12345;
    int m = 2147483648;
    
    vec2 o;
    
    float minDist = 10000000.0;
    
    for(int i = 0; i < NUM_POINTS; i++)
    {
        random = a * random + c;
        
        o.x = (float(random) / float(m)) * iResolution.x;
        
        random = a * random + c;
        
        o.y = (float(random) / float(m)) * iResolution.y;
        
        if(distance(fragCoord, o) < minDist)
        {
            minDist = distance(fragCoord, o);
            vec2 uv = o / iResolution.xy;
            uv.x = 1.0 - uv.x;
            fragColor = (texture(iChannel0, uv)) * (1.0 - minDist / 200.0);
        }
    }
}

void main(void) {
    mainImage(gl_FragColor, gl_FragCoord.xy);
}