/*{
	"DESCRIPTION": "droste",
	"CREDIT": "kaleidope.de",
	"CATEGORIES": [
		"mind expansion"
	],
	"INPUTS": [
		{
			"NAME": "iChannel0",
			"TYPE": "image"
		},
        {
            "NAME": "spiralfactor",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "NAME": "innerradius",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.1
        },
        {
            "NAME": "outerradius",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 2.0,
            "DEFAULT": 0.5
        },                
        {
            "NAME": "offset",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 2.0,
            "DEFAULT": 0.5
        },                
      	],
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);
float iTime = TIME;

float inv_webcam_aspect = 480.0/640.0,
    two_pi = 6.28318530718,
    inner_radius = innerradius,
    outer_radius = outerradius,
	spiral_factor = spiralfactor;

vec2 wrap(vec2 pos, float r1, float r2) {
    float theta = pos.x * two_pi,
        r = pos.y * (r2-r1) + r1;
    return vec2(offset + inv_webcam_aspect * r * cos(theta), 0.5 + r * sin(theta));
}

vec2 unwrap(vec2 pos, float factor) {
    vec2 centred = pos - vec2(0.5, 0.5);
    float theta = atan(centred.y, centred.x),
        phi = theta / two_pi,
        r2 = dot(centred, centred),
        logr = 0.5 * log(r2) * factor,
        y = logr - phi;
       
 	return vec2(phi, y - floor(y));   
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	vec2 uv = (fragCoord.xy - 0.5 * iResolution.xy) / iResolution.y + vec2(0.5, 0.5);
	fragColor = texture(iChannel0, wrap(unwrap(uv, spiral_factor), inner_radius, outer_radius));
}

void main(void) {
    mainImage(gl_FragColor, gl_FragCoord.xy);
}