/*{
	"DESCRIPTION": "pixelizer",
	"CREDIT": "shadertoy.com/user/IlyaLts",
	"CATEGORIES": [
		"mind expansion"
	],
	"INPUTS": [
		{
			"NAME": "iChannel0",
			"TYPE": "image"
		},
	    {
	        "NAME": "PIXEL_SIZE",
	        "TYPE": "float",
	        "MIN": 0.0,
	        "MAX": 20.0,
	        "DEFAULT": 10.0
	    },
      	],
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);

/* #define PIXEL_SIZE 10.0 */

void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    fragColor = vec4(0);
    
    float dx = 1.0 / iResolution.x;
    float dy = 1.0 / iResolution.y;
    uv.x = (dx * PIXEL_SIZE) * floor(uv.x / (dx * PIXEL_SIZE));
    uv.y = (dy * PIXEL_SIZE) * floor(uv.y / (dy * PIXEL_SIZE));
    
    for (int i = 0; i < int(PIXEL_SIZE); i++)
        for (int j = 0; j < int(PIXEL_SIZE); j++)
            fragColor += texture(iChannel0, vec2(uv.x + dx * float(i), uv.y + dy * float(j)));
    
    fragColor /= pow(PIXEL_SIZE, 1.8);
}

void main(void) {
    mainImage(gl_FragColor, gl_FragCoord.xy);
}