/*{
	"DESCRIPTION": "rotate control",
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
            "NAME": "swag1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
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
        {
            "NAME": "swag4",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 8.0,
            "DEFAULT": 2.0
        },        
        {
            "NAME": "swag5",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
          "NAME": "timeswag",
          "VALUES": [
            1.0,
            4.0,
            8.0
          ],
          "LABELS": [
            "slow",
            "medium",
            "fast"
          ],
          "DEFAULT": 1.0,
          "TYPE": "long"
        }
      	],
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);
float iTime = TIME;

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // Normalized pixel coordinates (from 0 to 1)
    vec2 c = (swag1 * fragCoord.xy - iResolution.xy) / iResolution.y;

    /**
     * uncomment as many or as few of the following lines as you like
     *
     * Or add your own of the form
     * c = some_function_of(c)
     */
    c = vec2(c.x * c.x - c.y * c.y, swag2 * c.x * c.y);
    // c = 8.0 * mat2(0.96, 0.28, -0.28, 0.96) * c;
    c = mat2(cos(iTime/timeswag), sin(iTime/timeswag), -sin(iTime/timeswag), cos(iTime/timeswag)) * c;
    // c = c + 0.1 * c * sin(8.0 * (length(c) + iTime));
    // c = 4.0 * c;
    /**
     * end of things to uncomment
     */
    
    c = mod(abs(mod(swag3 * (1.0 + c), vec2(swag4)) - vec2(1.0)), vec2(1.0));
    
    fragColor = texture(iChannel0, 0.5 * (swag5 + c));
}

void main(void) {
    mainImage(gl_FragColor, gl_FragCoord.xy);
}