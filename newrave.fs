/*{
	"DESCRIPTION": "new rave",
	"CREDIT": "shadertoy.com/user/netgrind",
	"CATEGORIES": [
		"mind expansion"
	],
	"INPUTS": [
		{
			"NAME": "iChannel0",
			"TYPE": "image"
		},
	    {
	        "NAME": "swag1",
	        "TYPE": "float",
	        "MIN": 0.0,
	        "MAX": 20.0,
	        "DEFAULT": 1.0
	    },
	    {
	        "NAME": "swag2",
	        "TYPE": "float",
	        "MIN": 0.0,
	        "MAX": 4.0,
	        "DEFAULT": 2.0
	    },
	    {
	        "NAME": "swag3",
	        "TYPE": "float",
	        "MIN": 0.1,
	        "MAX": 2.0,
	        "DEFAULT": 0.5
	    }, 		
      	],
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);
float iTime = TIME;

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    vec4 c = texture(iChannel0,uv);
    c = sin(uv.x*swag1+c*cos(c*swag2+iTime+uv.x)*sin(c+uv.y+iTime)*swag2)*swag3+swag3;
    c.b+=length(c.rg);
	fragColor = c;
}

void main(void) {
    mainImage(gl_FragColor, gl_FragCoord.xy);
}